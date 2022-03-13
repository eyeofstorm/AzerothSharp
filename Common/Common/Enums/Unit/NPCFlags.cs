/*
 * This file is part of the AzerothCore Project. See AUTHORS file for Copyright information
 *
 * This program is free software; you can redistribute it and/or modify it
 * under the terms of the GNU Affero General Public License as published by the
 * Free Software Foundation; either version 3 of the License, or (at your
 * option) any later version.
 *
 * This program is distributed in the hope that it will be useful, but WITHOUT
 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for
 * more details.
 *
 * You should have received a copy of the GNU General Public License along
 * with this program. If not, see <http://www.gnu.org/licenses/>.
 */

namespace AzerothSharp.Common;

[Flags]
public enum NPCFlags
{
    UNIT_NPC_FLAG_NONE = 0x0,
    UNIT_NPC_FLAG_GOSSIP = 0x1,
    UNIT_NPC_FLAG_QUESTGIVER = 0x2,
    UNIT_NPC_FLAG_VENDOR = 0x4,
    UNIT_NPC_FLAG_TAXIVENDOR = 0x8,
    UNIT_NPC_FLAG_TRAINER = 0x10,
    UNIT_NPC_FLAG_SPIRITHEALER = 0x20,
    UNIT_NPC_FLAG_GUARD = 0x40,
    UNIT_NPC_FLAG_INNKEEPER = 0x80,
    UNIT_NPC_FLAG_BANKER = 0x100,
    UNIT_NPC_FLAG_PETITIONER = 0x200,
    UNIT_NPC_FLAG_TABARDVENDOR = 0x400,
    UNIT_NPC_FLAG_BATTLEFIELDPERSON = 0x800,
    UNIT_NPC_FLAG_AUCTIONEER = 0x1000,
    UNIT_NPC_FLAG_STABLE = 0x2000,
    UNIT_NPC_FLAG_ARMORER = 0x4000
}
