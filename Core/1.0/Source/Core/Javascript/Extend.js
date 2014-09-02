(function () {
    if (!String.prototype.startsWith) {
        String.prototype.startsWith = function (a) {
            return this.substr(0, a.length) === a;
        }
    }
    if (!String.prototype.endsWith) {
        String.prototype.endsWith = function (a) {
            return this.substr(this.length - a.length) === a;
        }
    }
    if (!String.prototype.trimStart) {
        String.prototype.trimStart = function () {
            return this.replace(/^\s+/, "");
        }
    }
    if (!String.prototype.trimEnd) {
        String.prototype.trimEnd = function () {
            return this.replace(/\s+$/, "");
        }
    }
    if (!String.prototype.trim) {
        String.prototype.trim = function () {
            return this.replace(/^\s*/, '').replace(/\s*$/, '');
        }
    }
} ());