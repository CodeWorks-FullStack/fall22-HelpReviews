import { logger } from "../utils/Logger.js"

export class Restaurant {
  constructor (data) {
    this.id = data.id
    this.name = data.name
    this.description = data.description
    this.imgUrl = data.imgUrl
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
    this.creator = data.creator
    this.creatorId = data.creatorId
    this.tags = data.tags.length ? data.tags.split(',') : []
    this.exposure = data.exposure
    this.isShutdown = data.isShutdown
    this.reportCount = data.reportCount
  }
}