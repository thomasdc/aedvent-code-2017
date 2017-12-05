var input = 325489

size = roundToOdd((Math.sqrt(input)));

lowerbound = Math.pow((size - 2),2) + 1;
upperbound = size * size;

var idx = size - 2;
var vMap = {}

for (var i=lowerbound;i<=upperbound;i++){
    vMap[i] = idx;
    idx--;
    if(idx < 0) idx = size - 2
}

var row = size - 1
var col = vMap[input]
if(col == 0) col = size -1 //??

var center = Math.floor(size / 2)
var dist = Math.abs((row - center)) + Math.abs((col - center));

console.log(dist)

function roundToOdd(x){
    res = Math.ceil(x)
    if (res % 2 == 0) res++;
    return res;
}