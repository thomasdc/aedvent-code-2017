package main.scala.y2017.Day03

import main.scala.TestBase

class Tests extends TestBase {
  test("Puzzle 1") {
    val grid = Runner.generateGrid(277678)._1
    Runner.travelToCenter(grid, Runner.locate(grid, 277678)) shouldBe 475
  }

  test("Puzzle 2") {
    Runner.generateGrid(277678, summedValue = true)._2 shouldBe 279138
  }
}