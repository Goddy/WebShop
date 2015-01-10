var webShop = webShop || {};
webShop.utils = {
    isEmpty: function(text) {
        return text.trim() === "";
    },

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
        this.postOrGet("POST", url, data, func);
    }
};

webShop.searchProducts = (function($, utils) {
    return {
        search: function (pageNr, clearHtml) {
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
            return utils.jsonPost("/Products/_SearchPartial/", data, function (data) {
                if (clearHtml) {
                    resultDiv.html(data);
                    if (utils.isEmpty(data)) {
                        moreBtn.hide();
                    } else {
                        moreBtn.show();
                    }
                    
                } else {
                    if (utils.isEmpty(data)) {
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
    function replaceComma(text) {
        return text.replace(",", ".");
    }

    function replaceDots(text) {
        return text.replace(".", ",");
    }

    function updateTotal() {
        var total = 0.00;
        $(".amount").each(function () {
            var price = parseFloat(replaceComma($(this).next().text()));
            var amount = parseFloat($(this).val());
            total = parseFloat(total) + parseFloat(amount * price);
            total = parseFloat(total).toFixed(2);
        });
        total = replaceDots(total.toString());
        $(".grandTotal").text(total);
    }

    return {
        addToBasket: function (element) {
            var id = element.attr("href");
            var data = { productId: id };
            return utils.jsonPost("/Products/AddToBasket/", data, function () {
                console.log("Product added to session");
                $("#addToBasketModal").modal();
            });
        },
        updatePrices: function(element) {
            updateTotal();
        }
    }
}(jQuery, webShop.utils));

