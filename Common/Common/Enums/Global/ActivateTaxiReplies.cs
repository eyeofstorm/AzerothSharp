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

public enum ActivateTaxiReplies : byte
{
    ERR_TAXIOK = 0,
    ERR_TAXIUNSPECIFIEDSERVERERROR = 1,
    ERR_TAXINOSUCHPATH = 2,
    ERR_TAXINOTENOUGHMONEY = 3,
    ERR_TAXITOOFARAWAY = 4,
    ERR_TAXINOVENDORNEARBY = 5,
    ERR_TAXINOTVISITED = 6,
    ERR_TAXIPLAYERBUSY = 7,
    ERR_TAXIPLAYERALREADYMOUNTED = 8,
    ERR_TAXIPLAYERSHAPESHIFTED = 9,
    ERR_TAXIPLAYERMOVING = 10,
    ERR_TAXISAMENODE = 11,
    ERR_TAXINOTSTANDING = 12
}
