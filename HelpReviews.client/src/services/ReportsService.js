import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class ReportsService {
  async createReport(data, restaurantId) {
    const res = await api.post('api/restaurants/' + restaurantId + '/reports', data)
    logger.log(res.data)
  }
}

export const reportsService = new ReportsService()