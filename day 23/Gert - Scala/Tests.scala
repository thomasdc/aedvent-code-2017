package main.scala.y2017.Day23

import main.scala.TestBase
import scala.io.Source

class Tests extends TestBase {
  test("Puzzle 1") {
    val input = Source.fromFile("test/main/scala/y2017/Day23/puzzle.txt").getLines().toArray
    new Runner().run(input) shouldBe 9409
  }

  test("Puzzle 2") { new Runner().run2() shouldBe 913 }
}
