function calculateElo(botA, botB, winner) {
  const expectedScoreA =
    1 / (1 + Math.pow(10, (botB.elo - botA.elo) / 400));

  let scoreA;

  if (!winner) {
    scoreA = 0.5;
  } else if (winner === botA.name) {
    scoreA = 1;
  } else if (winner === botB.name) {
    scoreA = 0;
  }

  const delta = Math.round(64 * (scoreA - expectedScoreA));

  botA.elo += delta;
  botB.elo -= delta;
}