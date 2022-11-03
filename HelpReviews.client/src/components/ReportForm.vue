<template>
  <form @submit.prevent="createReport()">
    <div class="modal-body">
      <div class="form-floating mb-3">
        <input v-model="editable.title" required type="text" class="form-control" id="reportTitle"
          placeholder="Title...">
        <label for="reportTitle">Report Title</label>
      </div>
      <div class="form-floating mb-3">
        <textarea v-model="editable.body" required type="text" class="form-control" id="reportBody"
          placeholder="Body...">
          </textarea>
        <label for="reportBody">Report Body</label>
      </div>
      <label for="reportRating" class="form-label">Rating: {{ editable.rating }}</label>
      <input v-model="editable.rating" type="range" class="form-range" min="0" max="5" id="reportRating">
    </div>
    <div class="modal-footer">
      <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
      <button type="submit" class="btn btn-primary">Save changes</button>
    </div>
  </form>
</template>


<script>
import { ref } from 'vue';
import { logger } from '../utils/Logger.js';
import { reportsService } from '../services/ReportsService.js';
import Pop from '../utils/Pop.js';
import { useRoute } from 'vue-router';

export default {
  setup() {
    const editable = ref({ rating: 3 })
    const route = useRoute()
    return {
      editable,
      async createReport() {
        try {
          await reportsService.createReport(editable.value, route.params.restaurantId)
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>

</style>