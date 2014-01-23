'use strict';

function HomeViewModel(app, dataModel) {
    var self = this;

    var UTC = function (date) {
        return Date.UTC(
            date.getFullYear(),
            date.getMonth(),
            date.getDate(),
            date.getHours(),
            date.getMinutes(),
            date.getSeconds(),
            date.getMilliseconds());
    };

    var fetch = function () {
        dataModel.getImageContents().done(function (data) {
            for (var i in data) {
                var content = data[i];
                content.isOwned = ko.computed(function () {
                    return app.user().name() === this.userName;
                }, content);
                content.createdLocal = ko.computed(function () {
                    var date = new Date(UTC(new Date(this.created)));
                    return date.toDateString();
                }, content);
                content.updatedLocal = ko.computed(function () {
                    var date = new Date(UTC(new Date(this.updated)));
                    return date.toDateString();
                }, content);
                content.remove = function () {
                    dataModel.removeImageContent(this.id).done(function () {
                        setTimeout(fetch);
                    });
                };
            }
            self.contents(data);
        });
    };

    self.contents = ko.observableArray([]);

    self.newImage = {
        url: ko.observable(),
        description: ko.observable(),
        post: function () {
            console.log('URL: ' + self.newImage.url() + ", Description: " + self.newImage.description());
            dataModel.postImageContent({
                ImageUrl: self.newImage.url(),
                Description: self.newImage.description()
            }).done(function (data) {
                self.newImage.url('');
                self.newImage.description('');
                setTimeout(fetch);
            });
        }
    };

    setTimeout(fetch);
}

app.addViewModel({
    name: "Home",
    bindingMemberName: "home",
    factory: HomeViewModel
});
