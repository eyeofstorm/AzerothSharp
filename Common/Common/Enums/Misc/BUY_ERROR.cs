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

public enum BUY_ERROR : byte
{
    // SMSG_BUY_FAILED error
    // 0: cant find item
    // 1: item already selled
    // 2: not enought money
    // 4: seller(dont Like u)
    // 5: distance too far
    // 8: cant carry more
    // 11: level(require)
    // 12: reputation(require)

    BUY_ERR_CANT_FIND_ITEM = 0,
    BUY_ERR_ITEM_ALREADY_SOLD = 1,
    BUY_ERR_NOT_ENOUGHT_MONEY = 2,
    BUY_ERR_SELLER_DONT_LIKE_YOU = 4,
    BUY_ERR_DISTANCE_TOO_FAR = 5,
    BUY_ERR_CANT_CARRY_MORE = 8,
    BUY_ERR_LEVEL_REQUIRE = 11,
    BUY_ERR_REPUTATION_REQUIRE = 12
}
