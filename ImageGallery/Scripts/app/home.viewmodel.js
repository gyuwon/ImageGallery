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

    var extendContent = function (content) {
        content.isOwned = ko.computed(function () {
            return app.user() && app.user().name() === this.userName();
        }, content);
        content.createdLocal = ko.computed(function () {
            var date = new Date(UTC(new Date(this.created())));
            return date.toDateString();
        }, content);
        content.updatedLocal = ko.computed(function () {
            var date = new Date(UTC(new Date(this.updated())));
            return date.toDateString();
        }, content);
        content.remove = function () {
            dataModel.removeImageContent(this.id()).done(function () {
                self.contents.remove(content);
            });
        };
        var extendComment = function (comment) {
            comment.isOwned = ko.computed(function () {
                return app.user() && app.user().name() === this.userName();
            }, comment);
            comment.remove = function () {
                dataModel.removeImageContentComment(comment.id()).done(function () {
                    content.comments.remove(comment);
                });
            };
            return comment;
        };
        content.addComment = {
            comment: ko.observable(),
            add: function () {
                var comment = content.addComment.comment();
                console.log('ContentId: %s, Comment: %s', content.id(), comment);
                dataModel.postImageContentComment(content.id(), comment).done(function (data) {
                    content.addComment.comment('');
                    content.comments.push(extendComment(ko.mapping.fromJS(data)));
                });
            }
        };
        content.comments().forEach(extendComment);
        return content;
    };

    var fetch = function () {
        dataModel.getImageContents().done(function (data) {
            self.contents(data.map(function (content) {
                return extendContent(ko.mapping.fromJS(content));
            }));
        });
    };

    self.contents = ko.observableArray([]);

    self.newImage = {
        url: ko.observable(),
        description: ko.observable(),
        post: function () {
            console.log('URL: %s , Description: $s', self.newImage.url(), self.newImage.description());
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
