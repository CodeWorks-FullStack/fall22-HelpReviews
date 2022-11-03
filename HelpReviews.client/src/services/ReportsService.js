import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class ReportsService {
  async createReport(data, restaurantId) {
    const res = await api.post('api/restaurants/' + restaurantId + '/reports', data)
    logger.log(res.data)
    AppState.reports.push(res.data)
    AppState.restaurant.reportCount++
  }

  async getRestaurantReports(restaurantId) {
    const res = await api.get('api/restaurants/' + restaurantId + '/reports')
    logger.log(res.data)
    AppState.reports = res.data
  }

  async getMyReports() {
    const res = await api.get('account/__my-restaurant-reports__💓_BigJerms')
    logger.log('[Get My Reports]', res.data)
    AppState.myReports = res.data
  }
}

export const reportsService = new ReportsService()