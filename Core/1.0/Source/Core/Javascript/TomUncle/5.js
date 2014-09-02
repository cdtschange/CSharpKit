//原型使用方式1
var Calculator = function (decimalDigits, tax) {
    this.decimalDigits = decimalDigits;
    this.tax = tax;
}

Calculator.prototype = {
    add: function (x, y) {
        return x + y;
    },
    subtract: function (x, y) {
        return x - y;
    }
};

//原型使用方式2
Calculator.prototyp = function () {
    add = function (x, y) {
        return x + y;
    };
    subtract = function (x, y) {
        return x - y;
    }
    return {
        add: add,
        subtract: subtract
    }
} ();


var BaseCalculator = function () {
    this.decimalDigits = 2;
};
BaseCalculator.prototype = {
    add: function (x, y) {
        return x + y;
    },
    subtract: function (x, y) {
        return x - y;
    }
};

var Calculator = function () {
    this.tax = 5;
};

Calculator.prototype = new BaseCalculator();

var calc = new Calculator();
alert(calc.add(1, 1));
alert(calc.decimalDigits);

var Calculator = function () {
    this.tax = 5;
};

Calculator.prototype = BaseCalculator.prototype;

var calc = new Calculator();
alert(calc.add(1, 1));
alert(calc.decimalDigits); //undefined

Calculator.prototype.add = function (x, y) {
    return x + y + this.tax;
}
var calc = new Calculator();
alert(calc.add(1, 1));

function Foo() {
    this.value = 42;
}
Foo.prototype = {
    method: function () { }
};
function Bar() {

}
Bar.prototype = new Foo();
Bar.prototype.foo = 'Hello World';
//修正Bar.prototype.constructor為Bar本身
Bar.prototype.constructor = Bar;

var test = new Bar();


//先找屬性，再找原型鏈，再找Object原型鏈
function foo() {
    this.add = function (x, y) {
        return x + y;
    }
}
foo.prototype.add = function (x, y) {
    return x + y + 10;
}

Object.prototype.subtract = function (x, y) {
    return x - y;
}

var f = new foo();
alert(f.add(1, 2)); //3, not 13
alert(f.subtract(1, 2)); //-1

//foo.prototype = 1;//無效

//hasOwnProperty
Object.prototype.bar = 1;
var foo = { goo: undefined };
console.log(foo.bar); //1
console.log('bar' in foo); //true

console.log(foo.hasOwnProperty('bar')); //false
console.log(foo.hasOwnProperty('goo')); //true


var foo = {
    hasOwnProperty: function () {
        return false;
    },
    bar: 'Here be dragons'
};
console.log(foo.hasOwnProperty('bar')); //false

console.log({}.hasOwnProperty.call(foo, 'bar')); //true



Object.prototype.bar = 1;

var foo = { moo: 2 };
for (var i in foo) {
    console.log(i); //bar moo
}

for (var i in foo) {
    if (foo.hasOwnProperty(i))
        console.log(i); //moo
}








