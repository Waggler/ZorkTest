﻿using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace Zork
{
    public class World
    {
        public HashSet<Room> Rooms { get; set; }

        [JsonProperty]
        private string StartingLocation { get; set; }

        private Dictionary<string, Room> mRoomsByName;

        [JsonIgnore]
        public IReadOnlyDictionary<string, Room> RoomsByName => mRoomsByName;

        public Player SpawnPlayer() => new Player(this, StartingLocation);

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            mRoomsByName = Rooms.ToDictionary(room => room.Name, room => room);

            foreach (Room room in Rooms)
            {
                room.UpdateNeighbors(this);
            }
        }
    }
}