function ru__plural(word, num) {
    var forms = word.split('_');
    return num % 10 === 1 && num % 100 !== 11 ? forms[0] : (num % 10 >= 2 && num % 10 <= 4 && (num % 100 < 10 || num % 100 >= 20) ? forms[1] : forms[2]);
}
function ru__relativeTimeWithPlural(number, key) {
    var format = {
        'mm': 'минута_минуты_минут',
        'hh': 'час_часа_часов',
        'dd': 'день_дня_дней',
        'MM': 'месяц_месяца_месяцев',
        'yy': 'год_года_лет'
    };
    return number + ' ' + ru__plural(format[key], +number);
}

function Child(data) {
    var self = this;

    // api
    self.load = function (data) {
        if (data) ko.mapping.fromJSON(data, {}, self);
        self.Age = ko.computed(function () {
            var m = moment().startOf('day');
            if (m.isSameOrBefore(self.BirthDate())) {
                return '';
            };
            var years = m.diff(self.BirthDate(), "years");
            if (years > 0)
                return ru__relativeTimeWithPlural(years, 'yy');
            var months = m.diff(self.BirthDate(), "months");
            if (months > 0)
                return ru__relativeTimeWithPlural(months, 'MM');

            var days = m.diff(self.BirthDate(), "days");
            return ru__relativeTimeWithPlural(days, 'dd');

        }, self);
    };

    // init
    self.load(data);
}

function DiariesViewModel(childEmpty) {

    var self = this;

    self.newChild = new Child(childEmpty);
    self.newChild.BirthDate(moment().startOf('day').toDate());
    self.newChild.Sex('0');

    self.childs = ko.observableArray([]);

    self.getChilds = function () {
        self.childs.removeAll();
        $.getJSON('/child', function (data) {
            $.each(data, function (key, value) {
                self.childs.push(new Child(value));
            });
        });
    };

    self.saveChild = function (child) {
        return $.ajax({
            type: 'POST',
            url: '/child',
            data: ko.mapping.toJSON(child),
            contentType: 'application/json; charset=utf-8'
        });
    };

    // remove child. current data context object is passed to function automatically.
    self.removeChild = function (child) {
        $.ajax({
            url: '/child/' + child.ChildId(),
            type: 'delete',
            contentType: 'application/json',
            success: function () {
                self.childs.remove(child);
            }
        });
    };


    // init
    $.validator.setDefaults({
        submitHandler: function (form) {
            var context = ko.contextFor(form);
            context.saveChild(context.$data);
            return false; // for demo, blocks default submit, needed with ajax too.
        }
    });

    ko.applyBindings(self);
    //    self.getChilds();
}

