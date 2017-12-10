package main.scala.y2017.Day06

import main.scala.TestBase

class Tests extends TestBase {
  test("Example 1") { Runner.run(List(0,2,7,0)) shouldBe 5 }

  test("Puzzle 1") {
    Runner.run(List(14,0,15,12,11,11,3,5,1,6,8,4,9,1,8,4)) shouldBe 11137 }

  test("Example 2") { Runner.run(List(0,2,7,0), true) shouldBe 4 }

  test("Puzzle 2") {
    Runner.run(List(14,0,15,12,11,11,3,5,1,6,8,4,9,1,8,4), true) shouldBe 1037 }
}
