// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

namespace Orion.Items {
    /// <summary>
    /// Specifies an ammo type.
    /// </summary>
    public enum AmmoType {
        /// <summary>
        /// Indicates no ammo.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates gel. Used for the Flamethrower, for example.
        /// </summary>
        Gel = (int)ItemType.Gel,

        /// <summary>
        /// Indicates an arrow. Used for bows, for example.
        /// </summary>
        Arrow = (int)ItemType.WoodenArrow,

        /// <summary>
        /// Indicates a coin. Used for the Coin Gun, for example.
        /// </summary>
        Coin = (int)ItemType.CopperCoin,

        /// <summary>
        /// Indicates a fallen star. Used for the Star Cannon, for example.
        /// </summary>
        FallenStar = (int)ItemType.FallenStar,

        /// <summary>
        /// Indicates a bullet. Used for guns, for example.
        /// </summary>
        Bullet = (int)ItemType.MusketBall,

        /// <summary>
        /// Indicates a sand block. Used for the Sand Gun, for example.
        /// </summary>
        Sand = (int)ItemType.SandBlock,

        /// <summary>
        /// Indicates a dart. Used for the Blowpipe, for example.
        /// </summary>
        Dart = (int)ItemType.Seed,

        /// <summary>
        /// Indicates a rocket. Used for the Rocket Launcher, for example.
        /// </summary>
        Rocket = (int)ItemType.RocketI,

        /// <summary>
        /// Indicates a solution. Used for the Clentaminator, for example.
        /// </summary>
        Solution = (int)ItemType.GreenSolution,

        /// <summary>
        /// Indicates a flare. Used for the Flare Gun, for example.
        /// </summary>
        Flare = (int)ItemType.Flare,

        /// <summary>
        /// Indicates a snowball. Used for the Snowball Cannon, for example.
        /// </summary>
        Snowball = (int)ItemType.Snowball,

        /// <summary>
        /// Indicates a stynger bolt. Used for the Stynger, for example.
        /// </summary>
        StyngerBolt = (int)ItemType.StyngerBolt,

        /// <summary>
        /// Indicates candy corn. Used for the Candy Corn Rifle, for example.
        /// </summary>
        CandyCorn = (int)ItemType.CandyCorn,

        /// <summary>
        /// Indicates a jack 'o lantern. Used for the Jack 'O Lantern Launcher, for example.
        /// </summary>
        JackOLantern = (int)ItemType.ExplosiveJackOLantern,

        /// <summary>
        /// Indicates a stake. Used for the Stake Launcher, for example.
        /// </summary>
        Stake = (int)ItemType.Stake,

        /// <summary>
        /// Indicates a nail. Used for the Nail Gun, for example.
        /// </summary>
        Nail = (int)ItemType.Nail
    }
}
