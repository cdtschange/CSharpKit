var Calculator = function (eq) {
    var eqCtl = document.GetElementById(eq);
    return {
        add: function (x, y) {
            var val = x + y;
            eqCtl.innerHtml = val;
        }
    };
}

var calculator = new Calculator("eq");
calculator.add(2, 2);

var blogModule = (function (my) {
    var oldAddPhotoMethod = my.addPhoto;

    my.addPhoto = function () {
        //重載方法，依然可以通過oldAddPhotoMethod調用舊的方法
    };
    return my;
} (blogModule || {}));

//clone
var blogModule = (function (old) {
    var my = {},
        key;
    for (key in old) {
        if (old.hasOwnProperty) {
            my[key] = old[key];
        }
    }

    var oldAddPhotoMethod = my.addPhoto;

    my.addPhoto = function () {
        //重載方法，依然可以通過oldAddPhotoMethod調用舊的方法
    };
    return my;
} (blogModule || {}));

//跨文件共享私有對象
var blogModule = (function (old) {
    var _private = my._private = my._private || {},
    _seal = my._seal = my._seal || function () {
        delete my._private;
        delete my._seal;
        delete my._unseal;
    },
    _unseal = my._unseal = my._unseal || function () {
        my._private = _private;
        my._seal = _seal;
        my._unseal = _unseal;
    }
    return my;
} (blogModule || {}));

//子模塊
blogModule.CommentSubModule = (function () {
    var my = {};
    //...
    return my;
} ());