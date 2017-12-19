package main.scala.y2017.Day18

import main.scala.TestBase
import scala.io.Source

class Tests extends TestBase {
  test("Example 1") {
    val input = Source.fromFile("test/main/scala/y2017/Day18/example1.txt").getLines().toArray
    new Runner().run(input) shouldBe 4
  }

  test("Puzzle 1") {
    val input = Source.fromFile("test/main/scala/y2017/Day18/puzzle.txt").getLines().toArray
    new Runner().run(input) shouldBe 1187
  }

  test("Example 2") {
    val input = Source.fromFile("test/main/scala/y2017/Day18/example2.txt").getLines().toArray
    new Runner2().run(input) shouldBe 3
  }

  test("Puzzle 2") {
    val input = Source.fromFile("test/main/scala/y2017/Day18/puzzle.txt").getLines().toArray
    new Runner2().run(input) shouldBe 5969
  }
}
