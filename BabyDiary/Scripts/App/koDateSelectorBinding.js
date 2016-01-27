ko.bindingHandlers.dateSelector = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        //initialize datepicker with some optional options
        var options = allBindingsAccessor().dateSelectorOptions || {},
            $el = $(element);

        options.onDateChange = function () {
            var observable = valueAccessor();
            observable($el.dateSelector("getDate"));
        }
        $el.dateSelector(options);

        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $el.dateSelector("destroy");
            //$el.removeData();
        });

    },
    update: function (element, valueAccessor) {
        var value = ko.unwrap(valueAccessor()),
            $el = $(element);

        //        //handle date data coming via json from Microsoft
        //        if (String(value).indexOf('/Date(') == 0) {
        //            value = new Date(parseInt(value.replace(/\/Date\((.*?)\)\//gi, "$1")));
        //        }
        if (value != null) {
            var current = $(element).dateSelector("getDate");

            if (value - current !== 0) {
                $el.dateSelector("setDate", value);
            }
        }
    }
};