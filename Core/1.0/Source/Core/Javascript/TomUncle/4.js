function makeCounter() {
    var i = 0;
    return function () {
        console.log(++i);
    };
}

var counter = makeCounter();
counter(); //logs:1
counter(); //logs:2

var counter2 = makeCounter();
counter2(); //logs:1
counter2(); //logs:2
