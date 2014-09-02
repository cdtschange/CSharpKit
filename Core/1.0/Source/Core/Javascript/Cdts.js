Cdts = function () {

    return {
        //跨瀏覽器AddEvent
        addEvent: function () {
            var docE1 = document.documentElement;
            var fn;
            if (docE1.addEventListener) {
                fn = function addEvent(element, eventName, callback) {
                    element.addEventListener(eventName, callback, false);
                }
            } else if (docE1.attachEvent) {
                fn = function addEvent(element, eventName, callback) {
                    element.attachEvent("on" + eventName, callback);
                }
            } else {
                fn = function addEvent(element, eventName, callback) {
                    element["on" + eventName] = callback;
                }
            }
            var addEvent = null;
            return fn;
        },
        removeEvent: function () {
            var docE1 = document.documentElement;
            var fn;
            if (docE1.removeEventListener) {
                fn = function removeEvent(element, eventName, callback) {
                    element.removeEventListener(eventName, callback, false);
                }
            } else if (docE1.detachEvent) {
                fn = function removeEvent(element, eventName, callback) {
                    element.detachEvent("on" + eventName, callback);
                }
            } else {
                fn = function removeEvent(element, eventName, callback) {
                    element["on" + eventName] = null;
                }
            }
            var removeEvent = null;
            return fn;
        },
        randomsort: function (a, b) {
            return Math.random() > .5 ? -1 : 1; //用Math.random()函数生成0~1之间的随机数与0.5比较，返回-1或1
        }
    };
}
