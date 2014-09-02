//1
myglobal = "hello"; //不推薦寫法
console.log(myglobal); //"hello"
console.log(window.myglobal); //"hello"
console.log(window["myglobal"]); //"hello"
console.log(this.myglobal); //"hello"

var global_var = 1;
global_novar = 2; //反面教材
(function () {
    global_fromfunc = 3; //反面教材
} ());

delete global_var; //false
delete global_novar; //true
delete global_fromfunc; //true

console.log(typeof global_var); //"number"
console.log(typeof global_novar); //"undefined"
console.log(typeof global_fromfunc); //"undefined"

myname = "global";
function func() {
    alert(myname); //"undefined"
    var myname = "local";
    alert(myname); //"local"
}
func();

var man = {
    hands: 2,
    legs: 2,
    heads: 1
};
if (typeof (Object.prototype.clone === "undefined")) {
    Object.prototype.clone = function () { };
}
for (var i in man) {
    if (man.hasOwnProperty(i)) {//過濾
        console.log(i, ":", man[i]);
    }
}
//hands:2
//legs:2
//heads:1

//反面例子
for (var i in man) {
    console.log(i, ":", man[i]);
}
//hands:2
//legs:2
//heads:1
//clone: function()

//better way
var i, hasOwn = Object.prototype.hasOwnProperty;
for (var i in man) {
    if (hasOwn.call(man, i)) {//過濾
        console.log(i, ":", man[i]);
    }
}

if (typeof Object.prototype.myMethod !== "function") {
    Object.prototype.myMethod = function () {
        //實現...
    }
}

var zero = 0;
if (zero === false) {
    //不執行，因為zero為0，不是false
}
//反面事例
if (zero == false) {
    //執行了...
}

var obj = {
    name: 'ABC'
};
//反面事例
var property = "name";
alert(eval("obj." + property));

//更好的
alert(obj[property]);

console.log(typeof un); //"undefined"
console.log(typeof deux); //"undefined"
console.log(typeof trois); //"undefined"

var jsstring = "var un=1;console.log(un);";
eval(jsstring); //logs "1"
jsstring = "var deux=2; console.log(deux);";
new Function(jsstring)(); // logs "2"
jsstring = "var trois=3; console.log(trois);";
(function () {
    eval(jsstring);
})(); // logs "3"

console.log(typeof un); //"number"
console.log(typeof deux); //"undefined"
console.log(typeof trois); //"undefined"

(function () {
    var locala = 1;
    eval("locala =3; console.log(locala);")// logs "3"
    console.log(locala); // logs "3"
})();

(function () {
    var locala = 1;
    Function("console.log(typeof locala);")(); // logs undefined
})();

var month = "06",
    year = "09";
month = parseInt(month, 10);
year = parseInt(year, 10);

console.log(+"08"); // 8
console.log(Number("08")); // 8


//for
var myarray = [],
    i = myarray.length;
while (i--) {
};