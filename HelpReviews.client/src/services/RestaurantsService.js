import { AppState } from "../AppState.js"
import { Restaurant } from "../models/Restaurant.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class RestaurantsService {
  async getAllRestaurants() {
    const res = await api.get('api/restaurants')
    logger.log('[Get Restaurants]', res.data)
    AppState.restaurants = res.data.map(r => new Restaurant(r))
  }

  async createRestaurant(data) {
    const res = await api.post('api/restaurants', data)
    logger.log(res.data)
    AppState.restaurants.push(new Restaurant(res.data))
  }

  async removeRestaurant(id) {
    const res = await api.delete('api/restaurants/' + id)
    logger.log(res.data)
    AppState.restaurants = AppState.restaurants.filter(r => r.id !== id)
  }

  async getRestaurantById(id) {
    const res = await api.get('api/restaurants/' + id)
    logger.log(res.data)
    AppState.restaurant = new Restaurant(res.data)
    return res.data
  }

  async shutItDown(restaurantId) {
    const res = await api.put('api/restaurants/' + restaurantId + '/shut-it-down')
    logger.log('[SHUT IT DOWN 🚓]', res.data)
    AppState.restaurant = new Restaurant(res.data)
  }

}

export const restaurantsService = new RestaurantsService()