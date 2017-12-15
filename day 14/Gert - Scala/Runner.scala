package main.scala.y2017.Day14

object Runner {
  def run(input: String): Int = count(createGrid(input))

  def run2(input: String): Int = countRegions(createGrid(input))

  def count(grid: Array[Array[Int]]): Int =
    grid.map(row => row.count(cell => cell == 0)).sum

  def countRegions(initialGrid: Array[Array[Int]]): Int = {
    var grid = initialGrid
    while (thereAreUnmarkedPieces(grid))
      grid = markSector(grid)

    highestSector(grid)
  }

  def thereAreUnmarkedPieces(grid: Array[Array[Int]]): Boolean =
    grid.exists(row => row.contains(0))

  def markSector(grid: Array[Array[Int]]): Array[Array[Int]] = {
    val sector = highestSector(grid) + 1
    markOthers(markFirstPiece(grid, sector), sector)
  }

  def markFirstPiece(grid: Array[Array[Int]], sector: Int): Array[Array[Int]] = {
    grid.indices.foreach(rowIndex => {
      grid(rowIndex).indices.foreach(cellIndex => {
        if (grid(rowIndex)(cellIndex) == 0) {
          grid(rowIndex).update(cellIndex, sector)
          return grid
        }
      })
    })

    throw new Exception("Shouldn't have come here")
  }

  def markOthers(grid: Array[Array[Int]], sector: Int): Array[Array[Int]] = {
    var count, marked = 0

    do {
      marked = 0

      grid.indices.foreach(rowIndex => {
        grid(rowIndex).indices.foreach(cellIndex => {
          if (grid(rowIndex)(cellIndex) == 0 &&
            adjacentToSector(grid, rowIndex, cellIndex, sector)) {
            grid(rowIndex).update(cellIndex, sector)
            marked += 1
          }
        })
      })

      count += marked
    } while (marked != 0)

    grid
  }

  def adjacentToSector(grid: Array[Array[Int]], row: Int, cell: Int, sector: Int): Boolean = {
    (row - 1 >= 0 && grid(row - 1)(cell) == sector) ||
      (cell - 1 >= 0 && grid(row)(cell - 1) == sector) ||
      (cell + 1 < grid(row).length && grid(row)(cell + 1) == sector) ||
      (row + 1 < grid.length && grid(row + 1)(cell) == sector)
  }

  def highestSector(grid: Array[Array[Int]]): Int = grid.map(row => row.max).max

  def createGrid(input: String): Array[Array[Int]] =
    (0 to 127).map(seed => createRow("%s-%s".format(input, seed))).toArray

  def createRow(input: String): Array[Int] = toBits(knotHash(input))

  def knotHash(input: String): String = main.scala.y2017.Day10.Runner.run2(input)

  def toBits(hash: String): Array[Int] = hash.toCharArray.flatMap(char => toBits(char))

  def toBits(hexChar: Char): Array[Int] =
    Integer.toString(Integer.parseInt(hexChar.toString, 16), 2)
      .reverse.padTo(4, '0').reverse
      .toCharArray.map(mapToInt)

  def mapToInt(c: Char): Int =
    c match {
      case '1' => 0
      case _ => -1
    }
}
