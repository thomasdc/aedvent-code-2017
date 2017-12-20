const createStore = require('redux').createStore;
const readline = require('readline');
const rl = readline.createInterface({ input: require('fs').createReadStream('input.txt') });

/**
 * Load Maze
 */
const maze = [];
rl.on('line', function (line) {
    maze.push(line.split(''));
});

rl.on('close', function() {
    console.time('Run Maze');
    let result = run(maze);    
    console.timeEnd('Run Maze');
    console.log(`Result: ${JSON.stringify(result)}`);
});

/**
 * Helper
 */
const getSymbol = (maze, x, y) => maze[y][x].trim();
const moveState = (state, dir) => {
    const newState = {
        dir,
        x: state.x,
        y: state.y,
        steps: state.steps + 1
    };
    
    if (dir === 'up')      newState.y = state.y - 1; else
    if (dir === 'right')   newState.x = state.x + 1; else 
    if (dir === 'down')    newState.y = state.y + 1; else 
    if (dir === 'left')    newState.x = state.x - 1;    
    return newState;
}

/**
 * Run
 */
function run(maze) {
    let trail = '';

    function move(state, { type, val }) {
        switch (type) {
            case '|':
            case '-':
                return moveState(state, state.dir);
            case 'X':
            case '+':
                if (type === 'X') {
                    trail += val;
                }

                let paths = {
                    up: getSymbol(maze, state.x, state.y-1),
                    right: getSymbol(maze, state.x+1, state.y),
                    down: getSymbol(maze, state.x, state.y+1),
                    left: getSymbol(maze, state.x-1, state.y),
                };

                if (state.dir === 'up' && paths.up) {
                    return moveState(state, 'up');
                }                    
                if (state.dir === 'right' && paths.right) {
                    return moveState(state, 'right');
                }
                if (state.dir === 'down' && paths.down) {
                    return moveState(state, 'down');
                }
                if (state.dir === 'left' && paths.left) {
                    return moveState(state, 'left');
                }
                if (state.dir !== 'down' && paths.up) {
                    return moveState(state, 'up');
                }
                if (state.dir !== 'left' && paths.right) {
                    return moveState(state, 'right');
                }
                if (state.dir !== 'up' && paths.down) {
                    return moveState(state, 'down');
                }
                if (state.dir !== 'right' && paths.left) {
                    return moveState(state, 'left');
                }
                return Object.assign(state, { finished: true });            
            default:
                return state
        }
    }

    let store = createStore(move, { x: maze[0].indexOf('|'), y: 0, dir: 'down', steps: 1 });    
    do {
        let state = store.getState();
        if (state.finished) break;
        
        let symbol = getSymbol(maze, state.x, state.y);
        if (!symbol) throw new Error('Gone of the track');      
        if (['|', '+', '-'].includes(symbol)) {
            store.dispatch({ type: symbol });   
        } else {
            store.dispatch({ type: 'X', val: symbol });   
        }
    } while (true);

    return {
        trail,
        steps: store.getState().steps,
    };
}