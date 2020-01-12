using System;
using System.Linq;
using System.Collections.Generic;
using Codenation.Challenge.Exceptions;

namespace Codenation.Challenge
{
    public class SoccerPlayer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool isCaptain { get; set; }
        public long TeamId { get; set; }
        public int SkillLevel { get; set; }
        public decimal Salary { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class SoccerTeam
    {
        public SoccerTeam()
        {
            Players = new List<SoccerPlayer>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string MainShirtColor { get; set; }
        public string SecondaryShirtColor { get; set; }
        public List<SoccerPlayer> Players { get; set; }
    }

    public class SoccerTeamsManager : IManageSoccerTeams
    {
        private List<SoccerTeam> Teams;

        public SoccerTeamsManager()
        {
            Teams = new List<SoccerTeam>();
        }

        public void AddTeam(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor)
        {
            var teamExists = Teams.Any(x => x.Id == id);
            if (teamExists)
              throw new UniqueIdentifierException();
            
            Teams.Add(new SoccerTeam()
            {
                 Id = id,
                 Name = name,
                 CreateDate = createDate,
                 MainShirtColor = mainShirtColor,
                 SecondaryShirtColor = secondaryShirtColor
            });
        }

        public void AddPlayer(long id, long teamId, string name, DateTime birthDate, int skillLevel, decimal salary)
        {
            var team = Teams.Find(x => x.Id == teamId);
            if (team == null)
              throw new TeamNotFoundException();

            var playerExists = team.Players.Any(x => x.Id == id);
            if (playerExists)
              throw new UniqueIdentifierException();

            team.Players.Add(new SoccerPlayer(){
                Id = id,
                Name = name,
                BirthDate = birthDate,
                SkillLevel = skillLevel,
                Salary = salary
            });
        }

        public void SetCaptain(long playerId)
        {
            var team = Teams.Find(x => x.Players.Any(y => y.Id == playerId));
            if (team == null)
              throw new PlayerNotFoundException();

            // set every player in the team as regular player, except the requested "playerId"
            team.Players.ForEach(player => player.isCaptain = player.Id == playerId);
        }

        public long GetTeamCaptain(long teamId)
        {
            var team = Teams.Find(x => x.Id == teamId);
            if (team == null)
              throw new TeamNotFoundException();
            
            var captain = team.Players.Find(x => x.isCaptain);
            if (captain == null)
              throw new CaptainNotFoundException();
            
            return captain.Id;
        }

        public string GetPlayerName(long playerId)
        {
            var player = Teams.SelectMany(x => x.Players).ToList()
                .Find(x => x.Id == playerId);
                
            if (player == null)
              throw new PlayerNotFoundException();

            return player.Name;
        }

        public string GetTeamName(long teamId)
        {
            var team = Teams.Find(x => x.Id == teamId);
            if (team == null)
              throw new TeamNotFoundException();

            return team.Name;
        }

        public List<long> GetTeamPlayers(long teamId)
        {
            var team = Teams.Find(x => x.Id == teamId);
            if (team == null)
              throw new TeamNotFoundException();

            return team.Players
                .OrderBy(x => x.Id)
                .Select(x => x.Id)
                .ToList();
        }

        public long GetBestTeamPlayer(long teamId)
        {
            var team = Teams.Find(x => x.Id == teamId);
            if (team == null)
              throw new TeamNotFoundException();

            return team.Players
                .OrderByDescending(x => x.SkillLevel)
                .ThenBy(x => x.Id)
                .First().Id;
        }

        public long GetOlderTeamPlayer(long teamId)
        {
            var team = Teams.Find(x => x.Id == teamId);
            if (team == null)
              throw new TeamNotFoundException();

            return team.Players
                .OrderBy(x => x.BirthDate)
                .ThenBy(x => x.Id)
                .First().Id;
        }

        public List<long> GetTeams()
        {
            return Teams.Select(x => x.Id)
                .OrderBy(x => x).ToList();
        }

        public long GetHigherSalaryPlayer(long teamId)
        {
            var team = Teams.Find(x => x.Id == teamId);
            if (team == null)
              throw new TeamNotFoundException();

            return team.Players
                .OrderByDescending(x => x.Salary)
                .ThenBy(x => x.Id)
                .First().Id;
        }

        public decimal GetPlayerSalary(long playerId)
        {
            var player = Teams.SelectMany(x => x.Players).ToList()
                .Find(x => x.Id == playerId);
                
            if (player == null)
              throw new PlayerNotFoundException();

            return player.Salary;
        }

        public List<long> GetTopPlayers(int top)
        {
            return Teams.SelectMany(x => x.Players)
                .OrderByDescending(x => x.SkillLevel)
                .ThenBy(x => x.Id)
                .Take(top)
                .Select(x => x.Id)
                .ToList();
        }

        public string GetVisitorShirtColor(long teamId, long visitorTeamId)
        {
            var homeTeam = Teams.Find(x => x.Id == teamId);
            if (homeTeam == null)
              throw new TeamNotFoundException("home team not found");

            var visitorTeam = Teams.Find(x => x.Id == visitorTeamId);
            if (visitorTeam == null)
              throw new TeamNotFoundException("Visitor team not found");

            return homeTeam.MainShirtColor != visitorTeam.MainShirtColor
                ? visitorTeam.MainShirtColor
                : visitorTeam.SecondaryShirtColor;
        }

    }
}
