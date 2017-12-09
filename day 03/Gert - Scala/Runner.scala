package main.scala.y2017.Day03

object Runner extends Runner {}

class Runner {
  type Grid = Array[Array[Int]]
  type State = (Grid, Int, Int)

  val startValue = 1

  val north = (-1, 0)
  val east = (0, 1)
  val south = (1, 0)
  val west = (0, -1)

  def generateGrid(seed: Int, summedValue: Boolean = false): Grid = {
    val dimension = calculateDimension(seed)

    // Initialize grid w/ center
    val initialGrid = Array.ofDim[Int](dimension, dimension)
    initialGrid(dimension/2).update(dimension/2, startValue)

    // Start immediately right of the center to populate grid
    val initialState = (initialGrid, dimension/2, dimension/2 + 1)
    ((startValue + 1) to seed).foldLeft(initialState)((state, i) => {
      val (grid, x, y) = state

      val value = if(!summedValue) i else calculateValue(grid, x, y)

      if(value > seed)
        throw new Exception("First number larger than %s: %s".format(seed, value))

      grid(x).update(y, value)
      val direction = determineDirection(grid, x, y)

      (grid, x + direction._1, y + direction._2)
    })._1
  }

  def calculateValue(grid: Grid, x: Int, y: Int): Int = {
    val lowX = if(x - 1 >= 0) x - 1 else x
    val lowY = if(y - 1 >= 0) y - 1 else y
    val highX = if(x + 1 < grid.length) x + 1 else x
    val highY = if(y + 1 < grid(x).length) y + 1 else y

    (lowX to highX).flatMap(neighBourX =>
      (lowY to highY).map(neighBourY => grid(neighBourX)(neighBourY))
    ).sum
  }

  def calculateDimension(seed: Int): Int = Math.ceil(Math.sqrt(seed)).toInt

  def determineDirection(grid: Grid, x: Int, y: Int): (Int, Int) = {
    // Test which neighbours are already visited
    val left = y - 1 >= 0 && grid(x)(y - 1) != 0
    val above = x - 1 >= 0 && grid(x - 1)(y) != 0
    val right = y + 1 < grid(x).length && grid(x)(y + 1) != 0
    val under = x + 1 < grid.length && grid(x + 1)(y) != 0

    if(left && !above && !right) north
    else if(!left && !above && under) west
    else if(!left && right && !under) south
    else if(above && !right && !under) east
    else (0, 0)
  }

  def print(grid: Grid) = grid.foreach(row => println(row.mkString(",\t")))

  def locate(grid: Grid, value: Int): (Int, Int) = {
    for(x <- grid.indices)
      for(y <- grid(x).indices)
        if(grid(x)(y) == value)
          return (x, y)

    throw new Exception("Value %s not found in grid".format(value))
  }

  def lowestNeighbourPosition(grid: Grid, pos: (Int, Int)): (Int, Int) = {
    val (x, y) = pos

    val left = (valueOf(grid, x, y - 1), west)
    val above = (valueOf(grid, x - 1, y), north)
    val right = (valueOf(grid, x, y + 1), east)
    val under = (valueOf(grid, x + 1, y), south)

    if(left._1 <= above._1 && left._1 <= right._1 && left._1 <= under._1) left._2
    else if(above._1 <= left._1 && above._1 <= right._1 && above._1 <= under._1) above._2
    else if(right._1 <= above._1 && right._1 <= left._1 && right._1 <= under._1) right._2
    else if(under._1 <= above._1 && under._1 <= right._1 && under._1 <= left._1) under._2
    else throw new Exception("No lowest neighbour found")
  }

  def valueOf(grid: Grid, x: Int, y: Int): Int = {
    if (y < 0 || x < 0 || x >= grid.length || y >= grid(x).length || grid(x)(y) == 0)
      Int.MaxValue
    else
      grid(x)(y)
  }

  // Returns the number of steps needed
  def travelToCenter(grid: Grid, initialPosition: (Int, Int)): Int = {
    var position = initialPosition
    var steps = 0

    while (!isCenter(grid, position)) {
      steps = steps + 1
      val direction = lowestNeighbourPosition(grid, position)
      position = (position._1 + direction._1, position._2 + direction._2)
    }

    steps
  }

  def isCenter(grid: Grid, pos: (Int, Int)): Boolean = grid(pos._1)(pos._2) == startValue
}
