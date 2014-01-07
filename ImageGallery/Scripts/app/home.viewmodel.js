'use strict';

function HomeViewModel(app, dataModel) {
    var self = this;

    var fetch = function () {
        dataModel.getImageContents().done(function (data) {
            for (var i in data) {
                data[i].isOwned = function () {
                    return app.user().name() === this.userName;
                };
                data[i].remove = function () {
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
