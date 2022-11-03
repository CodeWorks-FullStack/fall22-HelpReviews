<template>
  <form @submit.prevent="createRestaurant()">
    <div class="modal-body">
      <div class="form-floating mb-3">
        <input v-model="editable.name" required type="text" class="form-control" id="restaurantName"
          placeholder="Name...">
        <label for="restaurantName">Restaurant Name</label>
      </div>
      <div class="form-floating">
        <textarea v-model="editable.description" required type="text" class="form-control" id="restaurantDescription"
          placeholder="Description...">
                </textarea>
        <label for="floatingPassword">Description</label>
      </div>
      <div class="form-floating mb-3">
        <input v-model="editable.imgUrl" required type="url" class="form-control" id="restaurantImg"
          placeholder="Image...">
        <label for="restaurantImg">Restaurant Image</label>
      </div>
      <div class="form-floating mb-3">
        <input v-model="editable.tags" required type="text" class="form-control" id="restaurantTags"
          title="Use commas and no spaces! I'm a great developer" placeholder="Tags...">
        <label for="restaurantTags">Tags</label>
      </div>
    </div>
    <div class="modal-footer">
      <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
      <button type="submit" class="btn btn-primary">Save changes</button>
    </div>
  </form>
</template>


<script>
import { ref } from 'vue';
import { restaurantsService } from '../services/RestaurantsService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createRestaurant() {
        try {
          await restaurantsService.createRestaurant(editable.value)
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