package main.scala.y2017.Day10

import main.scala.TestBase

class Tests extends TestBase {
  test("Example 1") { Runner.run(List(3, 4, 1, 5), 5) shouldBe 12 }

  test("Puzzle 1") {
    Runner.run(List(31,2,85,1,80,109,35,63,98,255,0,13,105,254,128,33)) shouldBe 6952
  }

  test("Creating hash") {
    Runner.createHash(Array(65,27,9,1,4,3,40,50,91,7,6,0,2,5,68,22)) shouldBe "40"
  }

  test("Example 2.1") { Runner.run2("") shouldBe "a2582a3a0e66e6e86e3812dcb672a272" }
  test("Example 2.2") { Runner.run2("AoC 2017") shouldBe "33efeb34ea91902bb2f59c9920caa6cd" }
  test("Example 2.3") { Runner.run2("1,2,3") shouldBe "3efbe78a8d82f29979031a4aa0b16a9d" }
  test("Example 2.4") { Runner.run2("1,2,4") shouldBe "63960835bcdc130f0b66d7ff4f6a5a8e" }

  test("Puzzle 2") {
    Runner.run2("31,2,85,1,80,109,35,63,98,255,0,13,105,254,128,33") shouldBe "28e7c4360520718a5dc811d3942cf1fd"
  }
}
