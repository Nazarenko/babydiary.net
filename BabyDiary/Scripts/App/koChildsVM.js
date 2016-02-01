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
    var mapping = {
        'BirthDate': {
            update: function (options) {
                if (options.data === null) {
                    return moment.utc().startOf('day').toDate();
                }
                if (_.isDate(options.data)) {
                    return options.data;
                }
                return moment.utc(options.data).toDate();
            }
        },
        'Sex': {
            update: function (options) {
                return options.data + '';
            }
        }
    }
    if (_.isObject(data)) {
        ko.mapping.fromJS(data, mapping, self);
    } else {
        ko.mapping.fromJSON(data, mapping, self);
    }

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

    self.FullName = ko.computed(function () {
        var fullname = self.LastName() + ' ' + self.FirstName() + ' ' + self.Surname();
        return fullname.trim();

    }, self);

}

function DiariesViewModel(childEmpty) {
    var self = this;

    self.newChild = childEmpty;

    self.currentEditable = ko.observable(null);

    self.childs = ko.observableArray([]).extend({ deferred: true });;

    self.getChilds = function () {
        self.childs.removeAll();
        $.getJSON('/childs', function (data) {
            $.each(data, function (key, value) {
                self.childs.push(new Child(value));
            });
        });
    };

    self.saveChild = function (form) {
//        if ($(form).valid()) {
            return $.ajax({
                type: 'POST',
                url: '/child',
                data: ko.mapping.toJSON(self.currentEditable()),
                contentType: 'application/json; charset=utf-8'
            });
//        }
    };

    self.editChild = function (child) {
        self.currentEditable(new Child(ko.mapping.toJS(child)));
    }

    self.addChild = function () {
        self.currentEditable(new Child(self.newChild));
    }

    self.addValidation = function () {
        //        $.validator.unobtrusive.parse($("#child-detail-form"));
        $("#child-detail-form").validateBootstrap(true);
        console.log('hbvhblj');
    }

    self.isInEditMode = function (child) {
        if (self.currentEditable() === null) {
            return false;
        }
        return child.ChildId() === self.currentEditable().ChildId();
    }

    self.isInAddMode = function () {
        if (self.currentEditable() === null) {
            return false;
        }
        return self.currentEditable().ChildId() === 0;
    }

    self.cancel = function () {
        self.currentEditable(null);
    }

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
        submitHandler: function () {
            self.saveChild();
            return false; // for demo, blocks default submit, needed with ajax too.
        }
    });

    ko.applyBindings(self);
    self.getChilds();
}

