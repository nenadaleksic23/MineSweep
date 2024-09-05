using Game.Models;

var mineField = MineFieldGame.GenerateMineField();
var player = new Player(mineField[0, 0]);
var game = new MineFieldGame(player, mineField);
game.Start();