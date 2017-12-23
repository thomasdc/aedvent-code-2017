const createStore = require('redux').createStore;
const readline = require('readline');
const fs = require('fs');
const rl = readline.createInterface({
  input: fs.createReadStream('input.txt')
});

let actions = [];
rl.on('line', function (line) {
    let parts = line.split(' ');
    actions.push({
        type: parts.shift(),
        args: parts,
    });
});

rl.on('close', function() {
    function duet(state, action) {
        switch (action.type) {
            case 'set':
                return state.set(action.args);
            case 'add':
                return state.add(action.args);
            case 'mul':
                return state.mul(action.args);
            case 'mod':
                return state.mod(action.args);
            default:
            return state
        }
    }

    let getState = (vals = {}) => {
        function getValue(val) {
            let valInt = parseInt(val)
            return (!isNaN(valInt)) ? valInt : (_arr[val] || 0);
        }

        let _arr = vals;

        return {
            _get: getValue,
            set: ([char, val]) => {
                _arr[char] = getValue(val);
                return getState(_arr);
            },
            snd: ([char]) => {
                console.log('Play ' + _arr[char]);
                return getState(_arr);
            },
            add: ([char, val]) => {
                _arr[char] = _arr[char] + getValue(val);
                return getState(_arr);
            },
            mul: ([char, val]) => {
                _arr[char] = _arr[char] * getValue(val);
                return getState(_arr);
            },
            mod: ([char, val]) => {
                _arr[char] = _arr[char] % getValue(val);
                return getState(_arr);
            },
        }
    };

    let isFlipping = false;
    let progIdx = 0;
    let programs = [
        { store: createStore(duet, getState({p: 0})), oprIdx: 0, queue: [], msgSent: 0 },
        { store: createStore(duet, getState({p: 1})), oprIdx: 0, queue: [], msgSent: 0 },
    ];

    do {
        let program = programs[progIdx];
        let operation = actions[program.oprIdx];
        
        console.log(`Program ${progIdx}: ${JSON.stringify(operation)}`);

        if (operation.type === 'snd') {
            program.msgSent++;
            programs[Math.abs(progIdx-1)].queue.push(program.store.getState()._get(operation.args[0]));            
        } else if (operation.type === 'rcv') {
            if (program.queue.length) {
                program.store.dispatch({ type: 'set', args: [operation.args[0], program.queue.shift()]});
            } else {
                if (isFlipping) {
                    console.log('Stooooop!')
                    break;
                }

                isFlipping = true;
                progIdx = Math.abs(progIdx-1);
                continue;
            }
        } else if (operation.type === 'jgz') {
            let X = program.store.getState()._get(operation.args[0]);
            let Y = program.store.getState()._get(operation.args[1]);
            if (X > 0 && Y !== 0) {
                program.oprIdx += Y;
                isFlipping = false;
                continue;
            }
        } else {
            program.store.dispatch(operation);  
        }

        program.oprIdx++;
        isFlipping = false;
    } while (true);

    console.log(`Program 1 sent ${programs[1].msgSent} messages.`)
});