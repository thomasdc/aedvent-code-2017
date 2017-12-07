"use strict";

const fs = require('fs');
const os = require('os');

function findMinMaxDiff(row){
    const items = row.split('\t');
    return Math.max(...items) -  Math.min(...items);
}

const input = fs.readFileSync("input.txt", "utf8");
const result = input.split(os.EOL).map(r => findMinMaxDiff(r)).reduce((prev, curr) => prev + curr);

console.log("Result: ", result);