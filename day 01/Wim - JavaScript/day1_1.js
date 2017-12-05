"use strict";

const fs = require('fs');

function getSum(digits){
    let sum = 0;
    for(let i = 0; i < digits.length; ++i){
        if (digits[i] == digits[(i+1) % digits.length]){
            sum += parseInt(digits[i]);
        } 
    }
    return sum;
}

const input = fs.readFileSync("input.txt", "utf8").split('');
const result = getSum(input);

console.log("Result: ", result);