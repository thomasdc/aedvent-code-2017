var lineReader = require('readline').createInterface({
    input: require('fs').createReadStream('pass.txt')
});

var list = [];

lineReader.on('line', function (line) {
    list.push(line.split(" "))
});


lineReader.on('close', function () {
    var valid = 0;

    list.forEach(function(passphrase){
        var map = {};
        var check = true;
        passphrase.forEach(function(word){
            //word = regularize(word) // ENABLE THIS FOR P2
            console.log(word.length);
            if(!map[word]) map[word] =1;
            else check = false;
        });
        if(check) valid++
    });
    console.log(valid);

});

var regularize = function(str) {
    return str.toLowerCase().split('').sort().join('').trim();
};