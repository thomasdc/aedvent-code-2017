package main.scala.y2017.Day03

object Runner {
  type Grid = Array[Array[Int]]
  type Pos = (Int, Int)
  type State = (Grid, Pos)

  val startValue = 1

  val north = (-1, 0)
  val east = (0, 1)
  val south = (1, 0)
  val west = (0, -1)

  def generateGrid(seed: Int, summedValue: Boolean = false): (Grid, Int) = {
    val dimension = calculateDimension(seed)
    var firstNumberLargerThanSeed = -1

    // Start immediately right of the center to populate grid
    val initialState = (initialGrid(dimension), initialPos(dimension))

    (((startValue + 1) to seed).foldLeft(initialState)((state, i) => {
      val (grid, pos) = state

      val value = if(!summedValue) i else calculateValue(grid, pos)

      if(value > seed && firstNumberLargerThanSeed == -1)
        firstNumberLargerThanSeed = value

      grid(pos._1).update(pos._2, value)
      val direction = determineDirection(grid, pos)

      (grid, (pos._1 + direction._1, pos._2 + direction._2))
    })._1, firstNumberLargerThanSeed)
  }

  def initialGrid(dimension: Int): Grid = {
    val grid = Array.ofDim[Int](dimension, dimension)
    grid(dimension/2).update(dimension/2, startValue)
    grid
  }

  def initialPos(dimension: Int): Pos = (dimension/2, dimension/2 + 1)

  def calculateValue(grid: Grid, pos: Pos): Int = {
    val lowX = if(pos._1 - 1 >= 0) pos._1 - 1 else pos._1
    val lowY = if(pos._2 - 1 >= 0) pos._2 - 1 else pos._2
    val highX = if(pos._1 + 1 < grid.length) pos._1 + 1 else pos._1
    val highY = if(pos._2 + 1 < grid(pos._1).length) pos._2 + 1 else pos._2

    (lowX to highX).flatMap(neighBourX =>
      (lowY to highY).map(neighBourY => grid(neighBourX)(neighBourY))
    ).sum
  }

  def calculateDimension(seed: Int): Int = Math.ceil(Math.sqrt(seed)).toInt

  def determineDirection(grid: Grid, pos: Pos): (Int, Int) = {
    // Test which neighbours are already visited
    val left = pos._2 - 1 >= 0 && grid(pos._1)(pos._2 - 1) != 0
    val above = pos._1 - 1 >= 0 && grid(pos._1 - 1)(pos._2) != 0
    val right = pos._2 + 1 < grid(pos._1).length && grid(pos._1)(pos._2 + 1) != 0
    val under = pos._1 + 1 < grid.length && grid(pos._1 + 1)(pos._2) != 0

    if(left && !above && !right) north
    else if(!left && !above && under) west
    else if(!left && right && !under) south
    else if(above && !right && !under) east
    else (0, 0)
  }

  def locate(grid: Grid, value: Int): Pos = {
    for(x <- grid.indices)
      for(y <- grid(x).indices)
        if(grid(x)(y) == value)
          return (x, y)

    throw new Exception("Value %s not found in grid".format(value))
  }

  def lowestNeighbourPosition(grid: Grid, pos: Pos): Pos = {
    val left = (valueOf(grid, (pos._1, pos._2 - 1)), west)
    val above = (valueOf(grid, (pos._1 - 1, pos._2)), north)
    val right = (valueOf(grid, (pos._1, pos._2 + 1)), east)
    val under = (valueOf(grid,(pos._1 + 1, pos._2)), south)

    if(left._1 <= above._1 && left._1 <= right._1 && left._1 <= under._1) left._2
    else if(above._1 <= left._1 && above._1 <= right._1 && above._1 <= under._1) above._2
    else if(right._1 <= above._1 && right._1 <= left._1 && right._1 <= under._1) right._2
    else if(under._1 <= above._1 && under._1 <= right._1 && under._1 <= left._1) under._2
    else throw new Exception("No lowest neighbour found")
  }

  def valueOf(grid: Grid, pos: Pos): Int = {
    if (pos._2 < 0 || pos._1 < 0 || pos._1 >= grid.length
      || pos._2 >= grid(pos._1).length || grid(pos._1)(pos._2) == 0)
      Int.MaxValue
    else
      grid(pos._1)(pos._2)
  }

  // Returns the number of steps needed
  def travelToCenter(grid: Grid, initialPosition: Pos): Int = {
    var position = initialPosition
    var steps = 0

    while (!isCenter(grid, position)) {
      val direction = lowestNeighbourPosition(grid, position)
      position = (position._1 + direction._1, position._2 + direction._2)
      steps += 1
    }

    steps
  }

  def isCenter(grid: Grid, pos: Pos): Boolean = grid(pos._1)(pos._2) == startValue
}
