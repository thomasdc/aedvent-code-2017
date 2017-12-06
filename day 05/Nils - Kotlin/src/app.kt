import java.io.File

fun main(args: Array<String>) {
    val maze:Array<Int> = File("input.txt").readLines().map({ value -> value.toInt() }).toTypedArray()
    println("Part 1: " + getMazeTime(maze.copyOf(), false))
    println("Part 2: " + getMazeTime(maze.copyOf(), true))
}

fun getMazeTime(maze: Array<Int>, weirdJumping: Boolean): Int {
    val endOfMaze:Int = maze.count()

    var index = 0
    var count = 0

    while (index in 0..(endOfMaze - 1)) {
        val jump = maze[index]
        if (weirdJumping && jump >= 3) {
            maze[index]--
        } else {
            maze[index]++
        }
        index += jump
        count++
    }

    return count
}