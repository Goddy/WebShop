var webShop = webShop || {};
webShop.utils = {
    postOrGet: function (type, url, data, func) {
        $.ajax({
            type: type,
            url: url,
            data: data,
            success: func,
            error: function (xhr, status, error) {
                console.log("status: " + status + ", error: " + error);
            },
            dataType: "text"
        });
    },
    jsonGet: function (url, data, func) {
        this.postOrGet("GET", url, data, func);
    },
    jsonPost: function (url, data, func) {
        console.log("Executed jsonPost");
        this.postOrGet("POST", url, data, func);
    }
};

webShop.searchProducts = (function($, utils) {
    return {
        search: function(pageNr, clearHtml) {
            var searchString = $("input.search-text").val();
            var price = [];
            var cat = [];
            var moreBtn = $("#more");
            var resultDiv = $("#result");

            $("input.search-category").each(function() {
                if ($(this).is(":checked")) cat.push($(this).attr("name"));
            });
            $("input.search-price").each(function() {
                if ($(this).is(":checked")) {
                    var str = $(this).val();
                    price = str.split(",");
                }
            });

            var data = { categories: cat, minPrice: price[0], maxPrice : price[1], searchText: searchString, page : pageNr };
            return utils.jsonPost("/Products/Search/", data, function (data) {
                if (clearHtml) {
                    resultDiv.html(data);
                    moreBtn.show();
                } else {
                    if (data.trim() === "") {
                        moreBtn.hide();
                    } else {
                        resultDiv.append(data);
                        var nextPage = parseInt(moreBtn.attr("href")) + 1;
                        moreBtn.attr("href", nextPage);
                    }
                }
            });
        }
    }
}(jQuery, webShop.utils));

webShop.basketUtils = (function ($, utils) {
    return {
        addToBasket: function (element) {
            var id = element.attr("href");
            var data = { productId: id };
            return utils.jsonPost("/Products/AddToBasket/", data, function () {
                console.log("Product added to session");
                $("#addToBasketModal").modal();
            });
        }
    }
}(jQuery, webShop.utils));

