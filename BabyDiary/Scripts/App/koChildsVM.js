function Child(data) {
    var self = this;

    // api
    self.load = function (data) {
        if (data) ko.mapping.fromJSON(data, {}, self);
        self.BirthDate(moment().subtract(1, 'day').toDate());
        self.Age = ko.computed(function () {
            var time = moment(self.BirthTime(), 'HH:mm');
            var m = moment(self.BirthDate()).startOf('day');
            if (time.isValid()) {
                m.hours(time.hours()).minutes(time.minutes());
            }
            return m.fromNow(true);
        }, self);
    };

    // init
    self.load(data);
}

function DiariesViewModel(childEmpty) {

    var self = this;

    self.newChild = new Child(childEmpty);
    //    self.newChild.BirthDate(new Date());

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
    ko.applyBindings(self);
    //    self.getChilds();
}

