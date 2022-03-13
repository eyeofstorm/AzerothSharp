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

public enum MovementFlags
{
    MOVEMENTFLAG_NONE = 0x0,
    MOVEMENTFLAG_FORWARD = 0x1,
    MOVEMENTFLAG_BACKWARD = 0x2,
    MOVEMENTFLAG_STRAFE_LEFT = 0x4,
    MOVEMENTFLAG_STRAFE_RIGHT = 0x8,
    MOVEMENTFLAG_LEFT = 0x10,
    MOVEMENTFLAG_RIGHT = 0x20,
    MOVEMENTFLAG_PITCH_UP = 0x40,
    MOVEMENTFLAG_PITCH_DOWN = 0x80,
    MOVEMENTFLAG_WALK = 0x100,
    MOVEMENTFLAG_JUMPING = 0x2000,
    MOVEMENTFLAG_FALLING = 0x4000,
    MOVEMENTFLAG_SWIMMING = 0x200000,
    MOVEMENTFLAG_ONTRANSPORT = 0x2000000,
    MOVEMENTFLAG_SPLINE = 0x4000000
}
