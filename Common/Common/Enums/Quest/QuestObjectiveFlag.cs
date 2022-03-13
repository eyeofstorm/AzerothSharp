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

[Flags] // These flags are custom and are only used for MangosVB
public enum QuestObjectiveFlag
{
    QUEST_OBJECTIVE_KILL = 1, // You have to kill creatures
    QUEST_OBJECTIVE_EXPLORE = 2, // You have to explore an area
    QUEST_OBJECTIVE_ESCORT = 4, // You have to escort someone
    QUEST_OBJECTIVE_EVENT = 8, // Something is required to happen (escort may be included in this one)
    QUEST_OBJECTIVE_CAST = 16, // You will have to cast a spell on a creature or a gameobject (spells on gameobjects are f.ex opening)
    QUEST_OBJECTIVE_ITEM = 32, // You have to recieve some items to deliver
    QUEST_OBJECTIVE_EMOTE = 64 // You do an emote to a creature
}
