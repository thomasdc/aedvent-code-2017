package main.scala.y2017.Day03

import main.scala.TestBase

import scala.io.Source

class Tests extends TestBase {
  test("Puzzle 1") {
    val sut = new Runner()

    val seed = 277678
    val grid = sut.generateGrid(seed)

    sut.travelToCenter(grid, sut.locate(grid, seed)) shouldBe 475
  }

  test("Puzzle 2") {
    // Ew dirty
    try {
      new Runner().generateGrid(277678, summedValue = true)
    } catch {
      case e: Exception => println(e.getMessage) // 279138
    }
  }
}
