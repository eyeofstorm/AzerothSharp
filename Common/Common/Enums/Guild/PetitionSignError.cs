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

public enum PetitionSignError
{
    PETITIONSIGN_OK = 0,                     // :Closes the window
    PETITIONSIGN_ALREADY_SIGNED = 1,         // You have already signed that guild charter
    PETITIONSIGN_ALREADY_IN_GUILD = 2,       // You are already in a guild
    PETITIONSIGN_CANT_SIGN_OWN = 3,          // You can's sign own guild charter
    PETITIONSIGN_NOT_SERVER = 4             // That player is not from your server
}
