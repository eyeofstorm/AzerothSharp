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
public enum GameObjectFlags : byte
{
    GO_FLAG_IN_USE = 0x1,                        // disables interaction while animated
    GO_FLAG_LOCKED = 0x2,                        // require key, spell, event, etc to be opened. Makes "Locked" appear in tooltip
    GO_FLAG_INTERACT_COND = 0x4,                 // cannot interact (condition to interact)
    GO_FLAG_TRANSPORT = 0x8,                     // any kind of transport? Object can transport (elevator, boat, car)
    GO_FLAG_UNK1 = 0x10,                         //
    GO_FLAG_NODESPAWN = 0x20,                    // never despawn, typically for doors, they just change state
    GO_FLAG_TRIGGERED = 0x40                    // typically, summoned objects. Triggered by spell or other events
}
