var ContactViewPlugin = function () {
};

ContactViewPlugin.prototype.show = function (successCallback, failureCallback) {

    return cordova.exec(successCallback,
        failureCallback,
        'ContactViewPlugin',
        'show',
        []);

};


if (!window.plugins) {
    window.plugins = {};
}
if (!window.plugins.contactViewPlugin) {
    window.plugins.contactViewPlugin = new ContactViewPlugin();
}