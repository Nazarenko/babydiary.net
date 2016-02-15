Child.prototype.toJSON = function () {
    var copy = ko.toJS(this);
    delete copy.Diaries;
    return copy;
}

function Child(data) {
    var self = this;
    self.ChildId = 0;
    self.FirstName = "";
    self.LastName = "";
    self.Surname = "";
    if (_.isUndefined(data)) {
        self.BirthDate = moment.utc().startOf("day").toDate();
    }
    self.BirthTime = "";
    self.BirthPlace = "";
    self.Sex = "m";

    self.load = function (data) {
        var mapping = {
            "observe": []
        }
        ko.mapping.fromJS(data, mapping, self);

        if (!_.isDate(self.BirthDate)) {
            self.BirthDate = moment.utc(data.BirthDate).toDate();
        }

        self.Age = ko.computed(function () {
            var m = moment().startOf("day");
            if (m.isSameOrBefore(self.BirthDate)) {
                return "";
            };
            var years = m.diff(self.BirthDate, "years");
            if (years > 0)
                return ru__relativeTimeWithPlural(years, "yy");
            var months = m.diff(self.BirthDate, "months");
            if (months > 0)
                return ru__relativeTimeWithPlural(months, "MM");

            var days = m.diff(self.BirthDate, "days");
            return ru__relativeTimeWithPlural(days, "dd");

        }, self);

        self.FullName = ko.computed(function () {
            var fullname = self.LastName + " " + self.FirstName + " " + self.Surname;
            return fullname.trim();

        }, self);
    };
    if (!_.isUndefined(data)) {
        self.load(data);
    }

    self.isEqual = function (child) {
        return ko.mapping.toJSON(self) === ko.mapping.toJSON(child);
    }

    //    self.toJSON = function(options) {
    //        return _.omit(this.attributes, ["Diaries"]);
    //    }
}

function DiariesViewModel() {
    var self = this;

    self.currentEditable = ko.observable(null);

    self.childs = ko.observableArray([]).extend({ deferred: true });;

    self.getChilds = function () {
        self.childs.removeAll();
        $.getJSON("/childs", function (data) {
            $.each(data, function (key, value) {
                self.childs.push(new Child(value));
            });
        });
    };

    self.saveChild = function () {
        var childId = self.currentEditable().ChildId;
        if (childId === 0 || !_.findWhere(self.childs(), { ChildId: childId }).isEqual(self.currentEditable())) {
            $.ajax({
                type: "POST",
                url: "/child",
                data: ko.mapping.toJSON(self.currentEditable()),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    self.cancel();
                    var newChild = new Child(data);
                    var oldChild = ko.utils.arrayFirst(self.childs(), function (child) {
                        return child.ChildId === newChild.ChildId;
                    });
                    if (oldChild == null) {
                        self.childs.push(newChild);
                    } else {
                        self.childs.replace(oldChild, newChild);
                    }
                }
            });
        }
    };

    self.editChild = function (child) {
        self.currentEditable(ko.mapping.toJS(child));
    }

    self.addChild = function () {
        self.currentEditable(new Child());
    }

    self.afterRenderChild = function (elements) {
        //        $("#child-detail-form").validateBootstrap(true);
        var $wrapper = $(elements[1]);
        var $form = $wrapper.find("form");
        $form.removeData("validator");
        $form.removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse($form);

        var $el = $form.find("#BirthDate");
        var options = { minYear: 1980, defaultValue: self.currentEditable().BirthDate };
        options.onDateChange = function () {
            self.currentEditable().BirthDate = $el.dateSelector("getDate");
        }
        $el.dateSelector(options);

        $form.find("#BirthTime").inputmask({ mask: "h:s" });

        $wrapper.show();
    }

    self.isInEditMode = function (child) {
        if (self.currentEditable() === null) {
            return false;
        }
        return child.ChildId === self.currentEditable().ChildId;
    }

    self.isInAddMode = function () {
        if (self.currentEditable() === null) {
            return false;
        }
        return self.currentEditable().ChildId === 0;
    }

    self.cancel = function () {
        self.currentEditable(null);
    }

    // remove child. current data context object is passed to function automatically.
    self.removeChild = function (child) {

        $("#remove-child-question").dialog({
            resizable: false,
            height: 200,
            modal: true,
            title: child.FullName(), 
            buttons: {
                "Удалить": function () {
                    $(this).dialog("close");
                    $.ajax({
                        url: "/child/" + child.ChildId,
                        type: "delete",
                        contentType: "application/json",
                        success: function () {
                            self.childs.remove(child);
                        }
                    });
                },
                "Отмена": function () {
                    $(this).dialog("close");
                }
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

