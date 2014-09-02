var introParagraph = document.getElementById('intro');

var allUnorderedLists = document.getElementsByTagName('ul');

var unorderedList = document.getElementsByTagName('ul')[0];

var allListItems = unorderedList.getElementsByTagName('li');

for (var i = 0, length = allListItems.length; i < length; i++) {
    console.log(allListItems[i].firstChild.data);
}

introParagraph.style.color = '#FF0000';
introParagraph.padding = '2px 3px 0 3px';
introParagraph.backgroundColor = '#FFF';
introParagraph.marginTop = '20px';

function changeStyle(elem, property, val) {
    elem.style[property] = val;
}

changeStyle(introParagraph, 'color', 'red');

introParagraph.innerHTML = 'New content for the <strong>amazing</strong> paragraph!';

introParagraph.innerHTML += '...some more content...';

var someText = 'This is the text I want to add';
var textNode = document.createTextNode(someText);
introParagraph.appendChild(textNode);

var myNewLink = document.createElement('a');
myNewLink.href = 'http://google.com';
myNewLink.appendChild(document.createTextNode('Visit Google'));
//<a href="http://google.com">Visit Google</a>

introParagraph.appendChild(myNewLink);

function insertAfter(target, bullet) {
    target.nextSibling ? target.parentNode.insertBefore(bullet, target.nextSibling) : target.parentNode.appendChild(bullet);
}

var myElement = document.getElementById('my-button');
function buttonClick() {
    alert('You just clicked the button!');
}

//myElement.onclick = buttonClick;

 Cdts.addEvent(myElement, 'click', function () {
     alert('You clicked me.');
     //Cdts.removeEvent(myElement, 'click', arguments.callee);
 });

function myEventHandler(e) {
    //兼容IE的代碼
    e = e || window.event;

    //防止默認行為
    if (e.preventDefault) {
        e.preventDefault();
    } else {
        e.returnValue = false;
    }

    //防止默認行為停止向上冒泡
    if (e.stopPropagation) {
        e.stopPropagation();
    } else {
        e.cancelBubble = true;
    }
}

var myTable = document.getElementById('my-table')
myTable.onclick = function (e) {
    e = e || window.event;
    var targetNode = e.target || e.srcElement;

    if (targetNode.nodeName.toLowerCase() === 'tr') {
        alert('You clicked a table row!');
    }
}