"use strict";
var DateFormatValueConverter = (function () {
    function DateFormatValueConverter() {
    }
    DateFormatValueConverter.prototype.toView = function (value, format) {
        if (!value) {
            return "";
        }
        return moment(value).format(format);
    };
    return DateFormatValueConverter;
}());
exports.DateFormatValueConverter = DateFormatValueConverter;
//# sourceMappingURL=date-format.js.map