var input = 325489;
input++;
var solved = false;

size = roundToOdd((Math.sqrt(input)));

var grid = Array(size).fill().map(() => Array(size).fill(0));

var cursor_x = Math.floor(size / 2);
var cursor_y = Math.floor(size / 2);

grid[cursor_x][cursor_y] = 1;

cursor_x++

traverse_grid();

function traverse_grid(){
    for(var n=3;n<=size;n=n+2){
        for(var up=1;up<n-1;up++) fill_value(cursor_x, cursor_y++)
        for(var left=1;left<n;left++) fill_value(cursor_x--, cursor_y);
        for(var down=1;down<n;down++) fill_value(cursor_x, cursor_y--);
        for(var right=1;right<n+1;right++) fill_value(cursor_x++, cursor_y);
    }
}

function fill_value(x,y){
    console.log("x: " + x + " y: "+y);
    //inspect neighbours
    var value = 0;
    if(x + 1 < size){
        value += grid[x+1][y];
        if(y + 1 < size) value += grid[x+1][y+1];
        if(y - 1 > 0) value += grid[x+1][y-1];
    }
    if(x - 1 > 0){
        value += grid[x-1][y];
        if(y + 1 < size) value += grid[x-1][y+1];
        if(y - 1 > 0) value += grid[x-1][y-1];
    }

    if(y + 1 < size) value += grid[x][y+1];
    if(y - 1 > 0) value += grid[x][y-1];
    grid[x][y] = value;

    if(value > input && !solved){
        console.log(value)
        solved = true;
    }
}

function roundToOdd(x){
    res = Math.ceil(x)
    if (res % 2 == 0) res++;
    return res;
}
