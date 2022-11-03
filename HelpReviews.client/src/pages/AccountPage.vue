<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12 col-md-6 p-3" v-for="r in myReports" :key="r.id">
        <ReportCard :report="r" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import ReportCard from '../components/ReportCard.vue';
export default {
  setup() {
    return {
      account: computed(() => AppState.account),
      myReports: computed(() => AppState.myReports)
    };
  },
  components: { ReportCard }
}
</script>

<style scoped>

</style>
