function foo() {
    return bar();
}
function bar() {
    return baz();
}
function baz() {
    debugger;
}
foo();

function foo2() {
    return bar2();
}
var bar2=function () {
    return baz2();
}
function baz2() {
    debugger;
}
foo2();

Object.prototype.x = "outer";
(function () {
    var x="inner";
    (function foo() {
        alert(x);//inner
    })();
})();