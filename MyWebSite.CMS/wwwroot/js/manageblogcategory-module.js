class ManageBlogCategory {
    CategoryTemplate = async (data) => {
        let activeClass;
        const response = await new Promise((resolve, reject) => {
            $.get("/ManageBlog/CheckCategoryExist", { categoryName: data.name }, resolve).fail(reject);
        });
        activeClass = response ? "text-primary" : "text-secondary";
        return `<tr data-title="${data.title}" data-level="${data.level}">
                    <td class="title text-secondary">
                        <input class="form-control ${activeClass} w-100" value="${data.title}"/>                      
                    </td>
                    <td>
                        <button type="button" class="btn btn-sm btn-danger">
                            <i class="fa-solid fa-trash"></i>
                        </button>
                    </td>
                </tr>`;
    }

    AppendListCategory = async () => {
        let self = this;
        let categoryObjArr = $("#gpt-data-visualizer").find("#category li").map(function (index) {
            return {
                title: $(this).text(),
                level: index + 1
            };
        }).get();
        $("input[id=Categories]").val(JSON.stringify(categoryObjArr));
        const templates = await Promise.all(categoryObjArr.map(self.CategoryTemplate));
        $("#category-list").html(templates.join(''));
        self.BuildCategoryData();
        self.RefreshSortable();
    }

    AddMoreCategory = () => {
        let self = this;
        $("#add-more-category").unbind("click");
        $("#add-more-category").click(async function () {
            let newCategory = {
                title: "",
                level: $("#category-list li").length
            };
            const template = await self.CategoryTemplate(newCategory);
            $("#category-list").append(template);
            self.RemoveCategory();
            self.BuildCategoryData();
            self.RefreshSortable();
            self.ChangeCategory();
        });
    }

    RefreshSortable = () => {
        let self = this;
        new Sortable(document.querySelector("#category-list"), {
            animation: 150,
            ghostClass: 'blue-background-class',
            onEnd: self.BuildCategoryData
        });
    }

    RemoveCategory = () => {
        $("#category-list tr .btn-danger").each(function () {
            $(this).unbind("click");
        });
        $("#category-list tr .btn-danger").click(function () {
            $(this).parents("tr").remove();
            self.BuildCategoryData();
        });
    }

    ChangeCategory = () => {
        $("#category-list input.form-control").change(function () {
            let parents = $(this).parents("tr");
            parents.attr("data-title", $(this).val());
        });
    }

    BuildCategoryData = () => {
        let arr = [];
        let i = 1;
        $("#category-list tr").each(function () {
            $(this)[0].dataset.level = i++;
            arr.push($(this)[0].dataset.title);
        });
        $("#Categories").val(arr.join(","));
    }

    Init = () => {
        this.AppendListCategory();
        this.AddMoreCategory();
        this.RemoveCategory();
        this.RefreshSortable();
        this.ChangeCategory();
    }
}
