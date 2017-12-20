var createStore = require('redux').createStore

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
            case 'snd':
                return state.snd(action.args);
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
        function print() { console.log(_arr); }
        function getValue(val) {
            let valInt = parseInt(val)
            return (!isNaN(valInt)) ? valInt : (_arr[val] || 0);
        }

        let _arr = vals;
        print();

        return {
            _get: getValue,
            _print: print,
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

    let store = createStore(duet, getState());
    let next = 0;
    do {
        if (actions[next].type === 'rcv') {
            let X = store.getState()._get(actions[next].args[0]);
            if (X > 0) {
                console.log(`RCV on position ${next} has value ${X}`);
                break;
            }
        } else if (actions[next].type === 'jgz') {
            let X = store.getState()._get(actions[next].args[0]);
            let Y = store.getState()._get(actions[next].args[1]);
            if (X > 0 && Y !== 0) {
                next += Y;
                continue;
            }
        } else {
            store.dispatch(actions[next]);  
        }

        next++;
    } while (true);

    store.getState()._print();
});